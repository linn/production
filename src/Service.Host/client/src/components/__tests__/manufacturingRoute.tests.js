import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingRoute from '../manufacturingRoutes/ManufacturingRoute';

afterEach(cleanup);

const addManufacturingRouteMock = jest.fn();
const updateManufacturingRouteMock = jest.fn();
const setEditStatusMock = jest.fn();
const manufacturingRoute = {
    resourceCode: 'TESTCODE1',
    description: 'Descripticon',
    cost: 10.55
};

const defaultProps = {
    loading: false,
    itemId: 'TESTCODE1',
    item: manufacturingRoute,
    editStatus: 'view',
    addItem: addManufacturingRouteMock,
    updateItem: updateManufacturingRouteMock,
    setEditStatus: setEditStatusMock
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
        const item = getByDisplayValue('TESTCODE1');
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
            resourceCode: 'TESTCODE1',
            description: '',
            cost: 12
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
        expect(updateManufacturingRouteMock).toHaveBeenCalledWith('TESTCODE1', manufacturingRoute);
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addManufacturingRoute', () => {
        const { getByText, getAllByDisplayValue } = render(
            <ManufacturingRoute {...defaultProps} item={{}} editStatus="create" />
        );

        // we need to fill the inputs before we are allowed to click save
        const inputs = getAllByDisplayValue('');
        inputs.forEach(input => {
            if (input.type !== 'number') {
                fireEvent.change(input, {
                    target: { value: 'new value' }
                });
            } else {
                fireEvent.change(input, {
                    target: { value: 1 }
                });
            }
        });

        // now click save
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(addManufacturingRouteMock).toHaveBeenCalled();
    });
});
