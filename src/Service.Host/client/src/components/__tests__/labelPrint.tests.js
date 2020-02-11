import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import LabelPrint from '../labelPrinting/LabelPrint';

afterEach(cleanup);

const print = jest.fn();
const searchAddresses = jest.fn();
const clearAddressSearch = jest.fn();
const searchSuppliers = jest.fn();
const clearSupplierSearch = jest.fn();

const labelPrintTypes = [
    { id: 0, name: 'wan' },
    { id: 2, name: 'two' },
    { id: 1, name: 'buckle ma shoe' },
    { id: 3, name: '3' },
    { id: 4, name: '4' }
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

    test('Should display form fields', () => {
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
