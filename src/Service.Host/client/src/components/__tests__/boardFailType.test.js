import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../test-utils';
import BoardFailType from '../boardFailTypes/BoardFailType';

afterEach(cleanup);

const addBoardFailTypeMock = jest.fn();
const updateBoardFailTypeMock = jest.fn();
const setEditStatusMock = jest.fn();
const failType = {
    failType: 1,
    description: 'description',
    dateInvalid: null
};

const defaultProps = {
    loading: false,
    itemId: '1',
    item: failType,
    editStatus: 'view',
    addItem: addBoardFailTypeMock,
    updateItem: updateBoardFailTypeMock,
    setEditStatus: setEditStatusMock,
    snackbarVisible: false,
    setSnackbarVisible: jest.fn(),
    history: {}
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<BoardFailType {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<BoardFailType {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<BoardFailType {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<BoardFailType {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(<BoardFailType {...defaultProps} />);
        const item = getByDisplayValue('1');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<BoardFailType {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<BoardFailType {...defaultProps} />);
        const input = getByDisplayValue('description');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<BoardFailType {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled and no description', () => {
        const noDescription = {
            failType: 1,
            description: ''
        };

        const { getByText } = render(
            <BoardFailType {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateBoardFailType and change set edit status to view', () => {
        const { getByText } = render(<BoardFailType {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateBoardFailTypeMock).toHaveBeenCalledWith('1', failType);
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addBoardFailType', () => {
        const { getByText, getAllByDisplayValue } = render(
            <BoardFailType {...defaultProps} item={{}} editStatus="create" />
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
        expect(addBoardFailTypeMock).toHaveBeenCalled();
    });
});
