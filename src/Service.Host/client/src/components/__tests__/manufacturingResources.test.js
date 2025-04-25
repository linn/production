import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingResources from '../manufacturingResources/ManufacturingResources';

afterEach(cleanup);

const manufacturingResources = [
    {
        resourceCode: 'TESTCODE1',
        description: 'Descripticon',
        cost: 10,
        dateInvalid: new Date().toISOString()
    },
    {
        resourceCode: 'TESTCODE2',
        description: 'Descrip',
        cost: 12,
        dateInvalid: new Date().toISOString()
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: manufacturingResources,
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<ManufacturingResources {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<ManufacturingResources {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(
            <ManufacturingResources {...defaultProps} loading={false} />
        );
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<ManufacturingResources {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });

    test('should display the two manufactruing resources', () => {
        const { getByText } = render(<ManufacturingResources {...defaultProps} />);
        const firstResource = getByText('TESTCODE1');
        const secondResource = getByText('TESTCODE2');
        expect(firstResource).toBeInTheDocument();
        expect(secondResource).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<ManufacturingResources {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });
});
