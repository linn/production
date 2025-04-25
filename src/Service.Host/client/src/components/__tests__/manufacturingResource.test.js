import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingResource from '../manufacturingResources/ManufacturingResource';

afterEach(cleanup);

const addManufacturingResourceMock = jest.fn();
const updateManufacturingResourceMock = jest.fn();
const setEditStatusMock = jest.fn();
const manufacturingResource = {
    resourceCode: 'TESTCODE1',
    description: 'Descripticon',
    cost: 10.55,
    dateInvalid: null
};

const defaultProps = {
    loading: false,
    itemId: 'TESTCODE1',
    item: manufacturingResource,
    editStatus: 'view',
    addItem: addManufacturingResourceMock,
    updateItem: updateManufacturingResourceMock,
    setEditStatus: setEditStatusMock,
    history: { push: jest.fn() },
    setSnackbarVisible: jest.fn()
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<ManufacturingResource {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<ManufacturingResource {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<ManufacturingResource {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<ManufacturingResource {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(<ManufacturingResource {...defaultProps} />);
        const item = getByDisplayValue('TESTCODE1');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<ManufacturingResource {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<ManufacturingResource {...defaultProps} />);
        const input = getByDisplayValue('Descripticon');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<ManufacturingResource {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button enabled and no description', () => {
        const noDescription = {
            resourceCode: 'TESTCODE1',
            description: '',
            cost: 12
        };

        const { getByText } = render(
            <ManufacturingResource {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateManufacturingResource and change set edit status to view', () => {
        const { getByText } = render(<ManufacturingResource {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateManufacturingResourceMock).toHaveBeenCalledWith(
            'TESTCODE1',
            manufacturingResource
        );
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addManufacturingResource', () => {
        const { getByText, getAllByDisplayValue } = render(
            <ManufacturingResource {...defaultProps} item={{}} editStatus="create" />
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
        expect(addManufacturingResourceMock).toHaveBeenCalled();
    });
});
