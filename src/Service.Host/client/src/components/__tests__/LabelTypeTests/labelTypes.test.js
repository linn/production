import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import LabelTypes from '../../labelTypes/LabelTypes';

afterEach(cleanup);

const labelTypes = [
    {
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
    },
    {
        labelTypeCode: 'test type code2',
        description: 'descrip 2',
        barcodePrefix: '22',
        nSBarcodePrefix: '27',
        filename: 'da file2',
        defaultPrinter: 'printr12',
        commandFilename: 'cmd filenom2',
        testFilename: 'test filenom2',
        testPrinter: 'tst printr2',
        testCommandFilename: 'tst cmd printr2'
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: labelTypes,
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<LabelTypes {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<LabelTypes {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(<LabelTypes {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<LabelTypes {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });

    test('should display the two label types', () => {
        const { getByText } = render(<LabelTypes {...defaultProps} />);
        const firstSkill = getByText('test type code');
        const secondSkill = getByText('test type code2');
        expect(firstSkill).toBeInTheDocument();
        expect(secondSkill).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<LabelTypes {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });
});
