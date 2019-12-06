import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionTriggerLevel from '../../productionTriggerLevels/TriggerLevel';

afterEach(cleanup);

const addProductionTriggerLevelMock = jest.fn();
const updateProductionTriggerLevelMock = jest.fn();
const setEditStatusMock = jest.fn();
const getWorkStationsMock = jest.fn();

const productionTriggerLevel = {
    partNumber: 'test partno',
    description: 'descrip yo',
    citCode: 'cit1',
    bomLevel: null,
    kanbanSize: 0,
    maximumKanbans: 0,
    overrideTriggerLevel: null,
    triggerLevel: null,
    variableTriggerLevel: null,
    workStationName: '1',
    temporary: 'Y',
    engineerId: 1,
    story: null,
    routeCode: '1'
};

const defaultProps = {
    loading: false,
    itemId: 'test partno',
    item: productionTriggerLevel,
    editStatus: 'view',
    addItem: addProductionTriggerLevelMock,
    updateItem: updateProductionTriggerLevelMock,
    setEditStatus: setEditStatusMock,
    history: {
        push: jest.fn()
    },
    setSnackbarVisible: jest.fn(),
    cits: [
        { code: '', name: '' },
        { code: 'cit1', name: 'a' },
        { code: 'cit2', name: 'b' }
    ],
    manufacturingRoutes: [{ routeCode: '1' }],
    employees: [{ id: 1, fullName: '' }],
    workStations: [{ workStationCode: '1', description: '' }],
    getWorkStationsForCit: getWorkStationsMock,
    itemErrors: { statusText: '' }
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
        const { queryByRole } = render(
            <ProductionTriggerLevel {...defaultProps} loading={false} />
        );
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
        const { getByText } = render(
            <ProductionTriggerLevel {...defaultProps} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled when no description', () => {
        const noDescription = {
            partNumber: 'test partno',
            description: '',
            citCode: 'cit1',
            bomLevel: null,
            kanbanSize: 0,
            maximumKanbans: 0,
            overrideTriggerLevel: null,
            triggerLevel: null,
            variableTriggerLevel: null,
            workStationName: '1',
            temporary: 'Y',
            engineerId: 1,
            story: null,
            routeCode: '1'
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
        const { getByText } = render(
            <ProductionTriggerLevel {...defaultProps} editStatus="edit" />
        );
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateProductionTriggerLevelMock).toHaveBeenCalledWith(
            'test partno',
            productionTriggerLevel
        );
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addProductionTriggerLevel', () => {
        const { getByText } = render(
            <ProductionTriggerLevel
                {...defaultProps}
                parts={[
                    { partNumber: 'test partno', description: '' },
                     { partNumber: '', description: '' }
                ]}
                item={{
                    partNumber: 'test partno',
                    description: 'descrip yo',
                    citCode: 'cit1',
                    bomLevel: null,
                    kanbanSize: 0,
                    maximumKanbans: 0,
                    overrideTriggerLevel: null,
                    triggerLevel: null,
                    variableTriggerLevel: null,
                    workStationName: '1',
                    temporary: 'Y',
                    engineerId: 1,
                    story: null,
                    routeCode: '1'
                }}
                editStatus="create"
            />
        );

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
