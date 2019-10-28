import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup, getByTestId } from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingRoute from '../manufacturingRoutes/ManufacturingRoute';

afterEach(cleanup);

const addManufacturingRouteMock = jest.fn();
const updateManufacturingRouteMock = jest.fn();
const setEditStatusMock = jest.fn();
const setSnackbarVisible = jest.fn();
const routeOperations = [
    {
        operationNumber: 101,
        description: 'op 1 desc',
        cITCode: 'op1CIT',
        skillCode: 'skillz1',
        setAndCleanTime: 13,
        resourceCode: 'resource1',
        cycleTime: 4,
        labourPercentage: 87
    },
    {
        operationNumber: 222,
        description: 'op 2 descrip',
        cITCode: 'op222CIT',
        skillCode: 'skillz2',
        setAndCleanTime: 22,
        resourceCode: 'resource2',
        cycleTime: 8,
        labourPercentage: 16
    }
];
const manufacturingRoute = {
    routeCode: 'TESTROUTECODE1',
    description: 'Descripticon',
    notes: 'test notes',
    operations: routeOperations
};
const manufacturingSkills = [
    {
        skillCode: 'skillz1',
        description: 'Descripticon',
        hourlyRate: 10
    },
    {
        skillCode: 'skillz2',
        description: 'Descrip',
        hourlyRate: 12
    }
];
const manufacturingResources = [
    {
        resourceCode: 'resource1',
        description: 'rsrc1',
        cost: 14
    },
    {
        resourceCode: 'resource2',
        description: 'rscr2',
        cost: 15
    }
];
const cits = [
    {
        code: 'op1CIT'
    },
    {
        code: 'op222CIT'
    }
];
const defaultProps = {
    loading: false,
    itemError: null,
    itemId: 'TESTROUTECODE1',
    item: manufacturingRoute,
    editStatus: 'view',
    addItem: addManufacturingRouteMock,
    updateItem: updateManufacturingRouteMock,
    setEditStatus: setEditStatusMock,
    history: {},
    snackbarVisible: false,
    manufacturingSkills,
    manufacturingResources,
    cits,
    setSnackbarVisible
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<ManufacturingRoute {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<ManufacturingRoute {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<ManufacturingRoute {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<ManufacturingRoute {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(<ManufacturingRoute {...defaultProps} />);
        const item = getByDisplayValue('TESTROUTECODE1');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<ManufacturingRoute {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<ManufacturingRoute {...defaultProps} />);
        const input = getByDisplayValue('Descripticon');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<ManufacturingRoute {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled and no description', () => {
        const noDescription = {
            resourceCode: 'TESTROUTECODE1',
            description: '',
            notes: 'some notes'
        };

        const { getByText } = render(
            <ManufacturingRoute {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateManufacturingRoute and change set edit status to view', () => {
        const { getByText } = render(<ManufacturingRoute {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateManufacturingRouteMock).toHaveBeenCalledWith(
            'TESTROUTECODE1',
            manufacturingRoute
        );
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

// describe('When creating', () => {
//     test('Should call addManufacturingRoute', () => {
//         const { getByText, getAllByDisplayValue } = render(
//             <ManufacturingRoute
//                 {...defaultProps}
//                 item={{ routeCode: '', description: '', notes: '', operations: routeOperations }}
//                 editStatus="create"
//             />
//         );

//         // we need to fill the inputs before we are allowed to click save
//         const inputs = getAllByDisplayValue('');
//         inputs.forEach(input => {
//             if (input.type !== 'number') {
//                 fireEvent.change(input, {
//                     target: { value: 'new value' }
//                 });
//             } else {
//                 fireEvent.change(input, {
//                     target: { value: 1 }
//                 });
//             }
//         });

//         //console.log(getByTestId(`inner0-0`));

//         // for (let i = 0; i < 8; i += 1) {
//         //     console.log(getByTestId(`inner0-${i}`).children[1].children[0].children[1].type);
//         //     fireEvent.change(getByTestId(`inner0-${i}`).children[1].children[0].children[1], {
//         //         target: { value: 'new value' }
//         //     });
//         // }

//         // now click save
//         fireEvent(
//             getByText('Save'),
//             new MouseEvent('click', {
//                 bubbles: true,
//                 cancelable: true
//             })
//         );
//         expect(addManufacturingRouteMock).toHaveBeenCalled();
//     });
// });
