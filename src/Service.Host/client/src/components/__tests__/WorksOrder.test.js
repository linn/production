import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import WorksOrder from '../worksOrders/WorksOrder';

afterEach(cleanup);

const addItemMock = jest.fn();
const updateItemMock = jest.fn();
const setEditStatusMock = jest.fn();

const worksOrder = {
    orderNumber: 827436,
    dateRaised: '2019-09-30T11:39:26.0000000',
    raisedBy: 33067,
    partNumber: 'KEEL',
    quantity: 12
};

const defaultProps = {
    editStatus: 'view',
    setSnackbarVisible: jest.fn(),
    fetchWorksOrderDetails: jest.fn(),
    setEditStatus: setEditStatusMock,
    addItem: addItemMock,
    updateItem: updateItemMock,
    history: { push: jest.fn() },
    fetchWorksOrder: jest.fn(),
    searchParts: jest.fn(),
    clearPartsSearch: jest.fn()
};

describe('when loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<WorksOrder {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<WorksOrder {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('when snackbar visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('when viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<WorksOrder {...defaultProps} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    it('should not show part search field', () => {
        const { queryByTitle } = render(
            <WorksOrder {...defaultProps} item={{}} editStatus="create" />
        );

        expect(queryByTitle('Search For Part')).toBeInTheDocument();
    });

    it('should display view specific form fields', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        expect(getByText('Works Order')).toBeInTheDocument();
        expect(getByText('Order Number')).toBeInTheDocument();
        expect(getByText('Quantity Built')).toBeInTheDocument();
        expect(getByText('Batch Number')).toBeInTheDocument();
        expect(getByText('Started By Shift')).toBeInTheDocument();
        expect(getByText('Raised By')).toBeInTheDocument();
        expect(getByText('Date Raised')).toBeInTheDocument();
        expect(getByText('Batch Number')).toBeInTheDocument();
        expect(getByText('Cancelled By')).toBeInTheDocument();
        expect(getByText('Date Cancelled')).toBeInTheDocument();
        expect(getByText('Reason Cancelled')).toBeInTheDocument();
        expect(getByText('Kitted Short')).toBeInTheDocument();
    });

    it('should display works order fields', () => {
        const { getByDisplayValue } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        expect(getByDisplayValue(worksOrder.orderNumber.toString())).toBeInTheDocument();
        expect(getByDisplayValue(worksOrder.partNumber)).toBeInTheDocument();
    });

    it('should display and format date field', () => {
        const { getByDisplayValue } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        expect(getByDisplayValue('30-Sep-2019')).toBeInTheDocument();
    });

    it('Should have save button disabled', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} item={worksOrder} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    it('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        const input = getByDisplayValue('12');
        fireEvent.change(input, {
            target: { value: '6' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('when creating', () => {
    it('should show part search field', () => {
        const { getByTitle } = render(
            <WorksOrder {...defaultProps} item={{}} editStatus="create" />
        );

        expect(getByTitle('Search For Part')).toBeInTheDocument();
    });

    it('should set doc type', () => {
        const { getByDisplayValue } = render(
            <WorksOrder {...defaultProps} item={{}} editStatus="create" />
        );

        expect(getByDisplayValue('WO')).toBeInTheDocument();
    });

    it('should not display view specific form fields', () => {
        const { queryByText } = render(
            <WorksOrder {...defaultProps} item={worksOrder} editStatus="create" />
        );

        expect(queryByText('Works Order')).toBeNull();
        expect(queryByText('Order Number')).toBeNull();
        expect(queryByText('Quantity Built')).toBeNull();
        expect(queryByText('Batch Number')).toBeNull();
        expect(queryByText('Started By Shift')).toBeNull();
        expect(queryByText('Raised By')).toBeNull();
        expect(queryByText('Date Raised')).toBeNull();
        expect(queryByText('Batch Number')).toBeNull();
        expect(queryByText('Cancelled By')).toBeNull();
        expect(queryByText('Date Cancelled')).toBeNull();
        expect(queryByText('Reason Cancelled')).toBeNull();
        expect(queryByText('Kitted Short')).toBeNull();
    });
});

describe('when editing', () => {
    it('should have save button disabled when works order has not been changed', () => {
        const { getByText } = render(
            <WorksOrder {...defaultProps} item={worksOrder} edotStatis="edit" />
        );
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    it('should have save button disabled when there is no quantity set', () => {
        const noQuantity = {
            orderNumber: 12345,
            quantity: null
        };

        const { getByText } = render(
            <WorksOrder {...defaultProps} item={noQuantity} edotStatis="edit" />
        );
        const item = getByText('Save');
        expect(item.closest('button')).toBeDisabled();
    });
});
