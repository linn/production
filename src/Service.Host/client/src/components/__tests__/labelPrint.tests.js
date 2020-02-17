import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import LabelPrint from '../labelPrinting/LabelPrint';

afterEach(cleanup);

const print = jest.fn();
const searchAddresses = jest.fn();
const clearAddressSearch = jest.fn();
const searchSuppliers = jest.fn();
const clearSupplierSearch = jest.fn();

const labelPrintTypes = [
    { id: 0, name: 'Large label (wee text)' },
    { id: 1, name: 'Large label (big text)' },
    { id: 2, name: 'Small' },
    { id: 3, name: 'PC Numbers' },
    { id: 4, name: 'Address Label' },
    { id: 5, name: 'Goods In Label' },
    { id: 6, name: 'Small (wee text)' },
    { id: 7, name: 'Small (wee bold text)' }
];

const labelPrinters = [
    { id: 3, name: 'three' },
    { id: 4, name: 'four' },
    { id: 1, name: 'knock on the door' },
    { id: 2, name: '2' },
    { id: 0, name: '0' }
];

const defaultProps = {
    loading: false,
    snackbarVisible: false,
    setSnackbarVisible: null,
    labelPrintTypes,
    labelPrinters,
    print,
    message: { data: { message: 'printed labels' } },
    searchAddresses,
    addressSearchLoading: false,
    addressSearchResults: [{}],
    clearAddressSearch,
    supplierSearchLoading: false,
    supplierSearchResults: [{}],
    searchSuppliers,
    clearSupplierSearch
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<LabelPrint {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<LabelPrint {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<LabelPrint {...defaultProps} snackbarVisible />);
        const item = getByText('printed labels');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<LabelPrint {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields for printer 0 (Large wee text)', () => {
        const { getByText } = render(<LabelPrint {...defaultProps} />);
        const item = getByText('Line 1');
        const item2 = getByText('Line 2');
        const item7 = getByText('Line 7');
        expect(item).toBeInTheDocument();
        expect(item2).toBeInTheDocument();
        expect(item7).toBeInTheDocument();
    });

    test('Should have print and clear buttons', () => {
        const { getByText } = render(<LabelPrint {...defaultProps} />);
        const printButton = getByText('Print');
        const clearButton = getByText('Clear');
        expect(printButton).toBeInTheDocument();
        expect(clearButton).toBeInTheDocument();
    });
});
