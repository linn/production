import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import WorksOrder from '../worksOrders/WorksOrder';

afterEach(cleanup);

const addItemMock = jest.fn();
const updateItemMock = jest.fn();
const setEditStatusMock = jest.fn();
const fetchWorksOrderDetailsMock = jest.fn();
const printWorksOrderLabelsMock = jest.fn();
const printWorksOrderAioLabelsMock = jest.fn();
const clearPrintWorksOrderLabelErrorsMock = jest.fn();
const clearPrintWorksOrderAioLabelErrorsMock = jest.fn();
const fetchSerialNumbersMock = jest.fn();

const worksOrder = {
    orderNumber: 827436,
    dateRaised: '2019-09-30T11:39:26.0000000',
    raisedBy: 33067,
    partNumber: 'KEEL',
    quantity: 12,
    docType: 'WO',
    workStationCode: 'CODE',
    raisedByDepartment: 'DEPT'
};

const defaultProps = {
    editStatus: 'view',
    setSnackbarVisible: jest.fn(),
    fetchWorksOrderDetails: fetchWorksOrderDetailsMock,
    setEditStatus: setEditStatusMock,
    addItem: addItemMock,
    updateItem: updateItemMock,
    history: { push: jest.fn() },
    fetchWorksOrder: jest.fn(),
    searchParts: jest.fn(),
    clearPartsSearch: jest.fn(),
    setPrintWorksOrderLabelsMessageVisible: jest.fn(),
    clearPrintWorksOrderLabelsErrors: clearPrintWorksOrderLabelErrorsMock,
    setPrintWorksOrderAioLabelsMessageVisible: jest.fn(),
    clearPrintWorksOrderAioLabelsErrors: clearPrintWorksOrderAioLabelErrorsMock,
    printWorksOrderAioLabels: printWorksOrderAioLabelsMock,
    printWorksOrderLabels: printWorksOrderLabelsMock,
    setDefaultWorksOrderPrinter: jest.fn(),
    clearErrors: jest.fn(),
    fetchSerialNumbers: fetchSerialNumbersMock,
    previousPath: '/path'
};

const employees = [
    {
        id: 33067,
        fullName: 'Employee Name'
    }
];

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

    it('should display employee full name', () => {
        const { getByDisplayValue } = render(
            <WorksOrder {...defaultProps} item={worksOrder} employees={employees} />
        );

        expect(getByDisplayValue('Employee Name')).toBeInTheDocument();
    });

    it('Should have save button disabled', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} item={worksOrder} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    it('Should request serial numbers', () => {
        render(<WorksOrder {...defaultProps} item={worksOrder} />);
        expect(fetchSerialNumbersMock).toHaveBeenCalled();
    });

    it('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        const input = getByDisplayValue('12');
        fireEvent.change(input, {
            target: { value: '6' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });

    it('should print labels', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        fireEvent(
            getByText('Print Labels'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        expect(printWorksOrderLabelsMock).toHaveBeenCalledWith({
            orderNumber: 827436,
            printerGroup: 'Prod'
        });
    });

    it('should print AIO labels', () => {
        const { getByText } = render(<WorksOrder {...defaultProps} item={worksOrder} />);

        fireEvent(
            getByText('Print AIO Labels'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        expect(printWorksOrderAioLabelsMock).toHaveBeenCalledWith({
            orderNumber: 827436
        });
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
            <WorksOrder {...defaultProps} item={{}} editStatus="create" />
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

    it('should have save disabled when fields not filled', () => {
        const { getByText } = render(
            <WorksOrder {...defaultProps} item={{}} editStatus="create" />
        );

        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    it('should display part search results when search button clicked', () => {
        const { getByText, getByTitle } = render(
            <WorksOrder
                {...defaultProps}
                item={{}}
                editStatus="create"
                partsSearchResults={[
                    { id: 'A', name: 'Part A', partNumber: 'Part A', description: 'Description A' },
                    { id: 'B', name: 'Part B', partNumber: 'Part B', description: 'Description B' }
                ]}
            />
        );

        fireEvent(
            getByTitle('Search For Part'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        expect(getByText('Part A')).toBeInTheDocument();
        expect(getByText('Description A')).toBeInTheDocument();
        expect(getByText('Part B')).toBeInTheDocument();
        expect(getByText('Description B')).toBeInTheDocument();
    });

    it('should fetch works order details when part is selected', () => {
        const { getByText, getByTitle } = render(
            <WorksOrder
                {...defaultProps}
                item={{}}
                editStatus="create"
                partsSearchResults={[
                    { id: 'A', name: 'Part A', partNumber: 'Part A', description: 'Description A' },
                    { id: 'B', name: 'Part B', partNumber: 'Part B', description: 'Description B' }
                ]}
            />
        );

        fireEvent(
            getByTitle('Search For Part'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        fireEvent(
            getByText('Part A'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        expect(fetchWorksOrderDetailsMock).toHaveBeenCalled();
    });

    it('should add item when all create fields are present', () => {
        const { getByText, getByTitle } = render(
            <WorksOrder
                {...defaultProps}
                item={{}}
                editStatus="create"
                worksOrderDetails={{
                    partNumber: 'PART',
                    workStationCode: 'AB',
                    departmentCode: 'DEPT',
                    quantityToBuild: 6
                }}
                partsSearchResults={[
                    { id: 'A', name: 'Part A', partNumber: 'Part A', description: 'Description A' },
                    { id: 'B', name: 'Part B', partNumber: 'Part B', description: 'Description B' }
                ]}
            />
        );

        fireEvent(
            getByTitle('Search For Part'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        fireEvent(
            getByText('Part A'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );

        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(addItemMock).toHaveBeenCalled();
    });
});

describe('when editing', () => {
    it('should have save button disabled when there is no quantity or reason cancelled', () => {
        const worksOrderNoQuantityOrReasonCancelled = {
            orderNumber: 827436,
            dateRaised: '2019-09-30T11:39:26.0000000',
            raisedBy: 33067,
            partNumber: 'KEEL',
            quantity: null,
            docType: 'WO',
            workStationCode: 'CODE',
            raisedByDepartment: 'DEPT',
            reasonCancelled: null
        };

        const { getByText } = render(
            <WorksOrder
                {...defaultProps}
                item={worksOrderNoQuantityOrReasonCancelled}
                editStatis="edit"
            />
        );
        const item = getByText('Save');
        expect(item.closest('button')).toBeDisabled();
    });

    it('should have save button enabled when editing item and quantity set', () => {
        const { getByText } = render(
            <WorksOrder {...defaultProps} item={worksOrder} editStatus="edit" />
        );

        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateItemMock).toHaveBeenCalled();
    });

    it('should have save button enabled when editing item and reason cancelled set', () => {
        const worksOrderNoQuantity = {
            orderNumber: 827436,
            dateRaised: '2019-09-30T11:39:26.0000000',
            raisedBy: 33067,
            partNumber: 'KEEL',
            quantity: null,
            docType: 'WO',
            workStationCode: 'CODE',
            raisedByDepartment: 'DEPT',
            reasonCancelled: 'reason'
        };

        const { getByText } = render(
            <WorksOrder {...defaultProps} item={worksOrderNoQuantity} editStatus="edit" />
        );

        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateItemMock).toHaveBeenCalled();
    });
});
