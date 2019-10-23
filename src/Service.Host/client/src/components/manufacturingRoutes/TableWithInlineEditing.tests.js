import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import Table from './TableWithInlineEditing';

afterEach(cleanup);

const manufacturingRoutes = [
    {
        resourceCode: 'TESTCODE1',
        description: 'Descripticon',
        cost: 10
    },
    {
        resourceCode: 'TESTCODE2',
        description: 'Descrip',
        cost: 12
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: manufacturingRoutes,
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<Table {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<Table {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(<Table {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<Table {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });

    test('should display the two manufactruing resources', () => {
        const { getByText } = render(<Table {...defaultProps} />);
        const firstRoute = getByText('TESTCODE1');
        const secondRoute = getByText('TESTCODE2');
        expect(firstRoute).toBeInTheDocument();
        expect(secondRoute).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<Table {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });
});
