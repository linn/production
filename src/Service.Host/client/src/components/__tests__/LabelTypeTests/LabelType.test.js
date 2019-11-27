import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import LabelType from '../../labelTypes/LabelType';

afterEach(cleanup);

const addLabelTypeMock = jest.fn();
const updateLabelTypeMock = jest.fn();
const setEditStatusMock = jest.fn();

const labelType = {
    labelTypeCode: 'test type code',
    description: 'descrip yo',
    barcodePrefix: '02',
    nSBarcodePrefix: '07',
    filename: 'da file',
    defaultPrinter: 'printr1',
    commandFilename: 'cmd filenom',
    testFilename: 'test filenom',
    testPrinter: 'tst printr',
    testCommandFilename: 'tst cmd printr'
};

const defaultProps = {
    loading: false,
    itemId: 'test type code',
    item: labelType,
    editStatus: 'view',
    addItem: addLabelTypeMock,
    updateItem: updateLabelTypeMock,
    setEditStatus: setEditStatusMock,
    history: {
        push: jest.fn()
    },
    setSnackbarVisible: jest.fn()
};

describe('When Loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<LabelType {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<LabelType {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('When Snackbar Visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<LabelType {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(<LabelType {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display form fields', () => {
        const { getByDisplayValue } = render(<LabelType {...defaultProps} />);
        const item = getByDisplayValue('test type code');
        expect(item).toBeInTheDocument();
    });

    test('Should have save button disabled', () => {
        const { getByText } = render(<LabelType {...defaultProps} />);
        const item = getByText('Save');
        expect(item.closest('button')).toHaveAttribute('disabled');
    });

    test('should change to edit mode on input', () => {
        const { getByDisplayValue } = render(<LabelType {...defaultProps} />);
        const input = getByDisplayValue('descrip yo');
        fireEvent.change(input, {
            target: { value: 'new value' }
        });
        expect(setEditStatusMock).toHaveBeenCalledWith('edit');
    });
});

describe('When Editing', () => {
    test('Should have save button enabled if input is Valid', () => {
        const { getByText } = render(<LabelType {...defaultProps} editStatus="edit" />);
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).not.toHaveAttribute('disabled');
    });

    test('Should have save button disabled when no description', () => {
        const noDescription = {
            labelTypeCode: 'test type code',
            description: '',
            barcodePrefix: '02',
            nSBarcodePrefix: '07',
            filename: 'da file',
            defaultPrinter: 'printr1',
            commandFilename: 'cmd filenom',
            testFilename: 'test filenom',
            testPrinter: 'tst printr',
            testCommandFilename: 'tst cmd printr'
        };

        const { getByText } = render(
            <LabelType {...defaultProps} item={noDescription} editStatus="edit" />
        );
        const item = getByText('Save');
        expect(item).toBeInTheDocument();
        expect(item.closest('button')).toHaveAttribute('disabled');
    });
});

describe('When updating', () => {
    test('Should call updateLabelType and change set edit status to view', () => {
        const { getByText } = render(<LabelType {...defaultProps} editStatus="edit" />);
        fireEvent(
            getByText('Save'),
            new MouseEvent('click', {
                bubbles: true,
                cancelable: true
            })
        );
        expect(updateLabelTypeMock).toHaveBeenCalledWith('test type code', labelType);
        expect(setEditStatusMock).toHaveBeenLastCalledWith('view');
    });
});

describe('When creating', () => {
    test('Should call addLabelType', () => {
        const { getByText, getAllByDisplayValue } = render(
            <LabelType {...defaultProps} item={{}} editStatus="create" />
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
        expect(addLabelTypeMock).toHaveBeenCalled();
    });
});
