import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import AteFaultCode from '../ate/AteFaultCode';

afterEach(cleanup);

const addAteFaultCodeMock = jest.fn();
const updateAteFaultCodeMock = jest.fn();
const setEditStatusMock = jest.fn();
const faultCode = {
    faultCode: 'F',
    description: 'fault code description',
    dateInvalid: null
};

const defaultProps = {
    loading: false,
    itemId: 'F',
    item: faultCode,
    editStatus: 'view',
    addItem: addAteFaultCodeMock,
    updateItem: updateAteFaultCodeMock,
    setEditStatus: setEditStatusMock,
    history: { push: jest.fn() },
    setSnackbarVisible: jest.fn()
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<AteFaultCode {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<AteFaultCode {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<AteFaultCode {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<AteFaultCode {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(

                <AteFaultCode {...defaultProps} /> 
            );
        const item = getByDisplayValue('F');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<AteFaultCode {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<AteFaultCode {...defaultProps} />);
        const input = getByDisplayValue('fault code description');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<AteFaultCode {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled and no description', () => {
        const noDescription = {
            faultCode: 'F',
            description: '',
            dateInvalid: null
        };

        const { getByText } = render(
            <AteFaultCode {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateAteFaultCode and change set edit status to view', () => {
        const { getByText } = render(<AteFaultCode {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateAteFaultCodeMock).toHaveBeenCalledWith('F', faultCode);
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addAteFaultCode', () => {
        const { getByText, getAllByDisplayValue } = render(
            <AteFaultCode {...defaultProps} item={{}} editStatus="create" />
        );

        // we need to fill the inputs before we are allowed to click save
        const inputs = getAllByDisplayValue('');
        inputs.forEach(input => {
            fireEvent.change(input, {
                target: { value: 'new value' }
            });
        });

        // now click save
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(addAteFaultCodeMock).toHaveBeenCalled();
    });
});
