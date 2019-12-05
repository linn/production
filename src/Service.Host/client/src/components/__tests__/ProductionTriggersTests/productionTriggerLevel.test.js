import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionTriggerLevel from '../../productionTriggerLevels/TriggerLevel';

afterEach(cleanup);

const addProductionTriggerLevelMock = jest.fn();
const updateProductionTriggerLevelMock = jest.fn();
const setEditStatusMock = jest.fn();

const productionTriggerLevel = {
    partNumber: 'test partno',
    description: 'descrip yo'
};

const defaultProps = {
    loading: false,
    itemId: 'test type code',
    item: productionTriggerLevel,
    editStatus: 'view',
    addItem: addProductionTriggerLevelMock,
    updateItem: updateProductionTriggerLevelMock,
    setEditStatus: setEditStatusMock,
    history: {
        push: jest.fn()
    },
    setSnackbarVisible: jest.fn()
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<ProductionTriggerLevel {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<ProductionTriggerLevel {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<ProductionTriggerLevel {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<ProductionTriggerLevel {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(<ProductionTriggerLevel {...defaultProps} />);
        const item = getByDisplayValue('test partno');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<ProductionTriggerLevel {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<ProductionTriggerLevel {...defaultProps} />);
        const input = getByDisplayValue('descrip yo');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<ProductionTriggerLevel {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled when no description', () => {
        const noDescription = {
            productionTriggerLevelCode: 'test partno',
            description: ''
        };

        const { getByText } = render(
            <ProductionTriggerLevel {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateProductionTriggerLevel and change set edit status to view', () => {
        const { getByText } = render(<ProductionTriggerLevel {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateProductionTriggerLevelMock).toHaveBeenCalledWith('test type code', productionTriggerLevel);
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addProductionTriggerLevel', () => {
        const { getByText, getAllByDisplayValue } = render(
            <ProductionTriggerLevel {...defaultProps} item={{}} editStatus="create" />
        );

        // we need to fill the inputs before we are allowed to click save
        const inputs = getAllByDisplayValue('');
        inputs.forEach(input => {
            if (input.type !== 'number') {
                fireEvent.change(input, {
                    target: { value: 'tx' }
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
        expect(addProductionTriggerLevelMock).toHaveBeenCalled();
    });
});
