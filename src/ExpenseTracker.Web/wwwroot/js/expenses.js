/**
 * Expense Tracker - Expenses JavaScript
 * Provides client-side functionality for expense management
 */

(function ($) {
    'use strict';

    // ============================================
    // Real-time Table Search/Filter
    // ============================================
    function initializeTableSearch() {
        const searchInput = $('#expenseSearch');
        const table = $('#expensesTable');
        const rows = table.find('tbody tr');

        if (searchInput.length === 0 || table.length === 0) return;

        searchInput.on('keyup', function () {
            const searchTerm = $(this).val().toLowerCase();

            rows.each(function () {
                const row = $(this);
                const text = row.text().toLowerCase();

                if (text.includes(searchTerm)) {
                    row.show();
                } else {
                    row.hide();
                }
            });

            // Update "no results" message
            updateNoResultsMessage(searchTerm);
        });
    }

    function updateNoResultsMessage(searchTerm) {
        const table = $('#expensesTable');
        const visibleRows = table.find('tbody tr:visible').length;
        let noResultsRow = table.find('.no-results-row');

        if (visibleRows === 0 && searchTerm) {
            if (noResultsRow.length === 0) {
                const colCount = table.find('thead th').length;
                noResultsRow = $('<tr class="no-results-row"><td colspan="' + colCount + '" class="text-center text-muted py-4"><i class="fas fa-search me-2"></i>No expenses match your search</td></tr>');
                table.find('tbody').append(noResultsRow);
            }
            noResultsRow.show();
        } else {
            noResultsRow.hide();
        }
    }

    // ============================================
    // Delete Confirmation
    // ============================================
    function initializeDeleteConfirmation() {
        $(document).on('click', '.delete-expense', function (e) {
            e.preventDefault();
            
            const deleteUrl = $(this).attr('href');
            const description = $(this).data('description') || 'this expense';

            if (confirm(`Are you sure you want to delete "${description}"?\n\nThis action cannot be undone.`)) {
                window.location.href = deleteUrl;
            }
        });

        // Alternative: Bootstrap modal confirmation (if modal exists)
        $(document).on('click', '.delete-expense-modal', function (e) {
            e.preventDefault();
            
            const deleteUrl = $(this).attr('href');
            const description = $(this).data('description') || 'this expense';
            
            $('#deleteModal').find('.expense-description').text(description);
            $('#deleteModal').find('#confirmDeleteBtn').attr('href', deleteUrl);
            $('#deleteModal').modal('show');
        });
    }

    // ============================================
    // Currency Input Formatter
    // ============================================
    function initializeCurrencyFormatter() {
        const currencyInputs = $('input[type="text"][name*="Amount"], input[type="number"][name*="Amount"]');

        if (currencyInputs.length === 0) return;

        currencyInputs.each(function () {
            const input = $(this);

            // Format on blur (when user leaves the field)
            input.on('blur', function () {
                formatCurrency(input);
            });

            // Remove formatting on focus (allow easy editing)
            input.on('focus', function () {
                let value = input.val().replace(/[^0-9.]/g, '');
                input.val(value);
            });

            // Allow only numbers and decimal point
            input.on('keypress', function (e) {
                const charCode = e.which ? e.which : e.keyCode;
                const value = $(this).val();

                // Allow: backspace, delete, tab, escape, enter, decimal point
                if (charCode === 46 || charCode === 8 || charCode === 9 || charCode === 27 || charCode === 13) {
                    // Only allow one decimal point
                    if (charCode === 46 && value.indexOf('.') !== -1) {
                        e.preventDefault();
                        return false;
                    }
                    return true;
                }

                // Allow only numbers
                if (charCode < 48 || charCode > 57) {
                    e.preventDefault();
                    return false;
                }

                return true;
            });
        });
    }

    function formatCurrency(input) {
        let value = input.val().replace(/[^0-9.]/g, '');
        
        if (value === '' || value === '.') {
            input.val('');
            return;
        }

        // Parse as float and format
        const numValue = parseFloat(value);
        
        if (isNaN(numValue)) {
            input.val('');
            return;
        }

        // Format with commas and 2 decimal places
        const formatted = '$' + numValue.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        input.val(formatted);
    }

    // ============================================
    // Client-side Date Validation
    // ============================================
    function initializeDateValidation() {
        const dateInputs = $('input[type="date"][name*="ExpenseDate"]');

        if (dateInputs.length === 0) return;

        dateInputs.on('change blur', function () {
            validateDate($(this));
        });

        // Validate on page load
        dateInputs.each(function () {
            validateDate($(this));
        });
    }

    function validateDate(input) {
        const dateValue = input.val();
        
        if (!dateValue) {
            setFieldError(input, 'Expense date is required');
            return false;
        }

        const selectedDate = new Date(dateValue);
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        if (selectedDate > today) {
            setFieldError(input, 'Expense date cannot be in the future');
            return false;
        }

        // Validate date is not too far in the past (e.g., 10 years)
        const tenYearsAgo = new Date();
        tenYearsAgo.setFullYear(today.getFullYear() - 10);

        if (selectedDate < tenYearsAgo) {
            setFieldError(input, 'Expense date seems too far in the past');
            return false;
        }

        clearFieldError(input);
        return true;
    }

    // ============================================
    // Form Field Validation Highlighting
    // ============================================
    function initializeFieldValidation() {
        const forms = $('form[data-validate="true"]');

        if (forms.length === 0) return;

        forms.on('submit', function (e) {
            let isValid = true;

            // Validate required fields
            $(this).find('[required]').each(function () {
                const field = $(this);
                
                if (!field.val() || field.val().trim() === '') {
                    setFieldError(field, 'This field is required');
                    isValid = false;
                } else {
                    clearFieldError(field);
                }
            });

            // Validate amount fields
            $(this).find('input[name*="Amount"]').each(function () {
                const field = $(this);
                let value = field.val().replace(/[^0-9.]/g, '');
                const numValue = parseFloat(value);

                if (isNaN(numValue) || numValue <= 0) {
                    setFieldError(field, 'Amount must be greater than 0');
                    isValid = false;
                }
            });

            // Validate date fields
            $(this).find('input[type="date"]').each(function () {
                if (!validateDate($(this))) {
                    isValid = false;
                }
            });

            if (!isValid) {
                e.preventDefault();
                
                // Scroll to first error
                const firstError = $('.is-invalid:first');
                if (firstError.length > 0) {
                    $('html, body').animate({
                        scrollTop: firstError.offset().top - 100
                    }, 300);
                }
            }
        });

        // Clear errors on input
        $('form input, form select, form textarea').on('input change', function () {
            clearFieldError($(this));
        });
    }

    function setFieldError(field, message) {
        field.addClass('is-invalid');
        field.removeClass('is-valid');

        // Remove existing error message
        field.siblings('.invalid-feedback').remove();

        // Add error message
        if (message) {
            const errorDiv = $('<div class="invalid-feedback d-block"></div>').text(message);
            field.after(errorDiv);
        }
    }

    function clearFieldError(field) {
        field.removeClass('is-invalid');
        field.addClass('is-valid');
        field.siblings('.invalid-feedback').remove();
    }

    // ============================================
    // Initialize on Document Ready
    // ============================================
    $(document).ready(function () {
        initializeTableSearch();
        initializeDeleteConfirmation();
        initializeCurrencyFormatter();
        initializeDateValidation();
        initializeFieldValidation();

        // Clear 'is-valid' class on first focus (cosmetic improvement)
        $('input.is-valid').one('focus', function () {
            $(this).removeClass('is-valid');
        });
    });

})(jQuery);
