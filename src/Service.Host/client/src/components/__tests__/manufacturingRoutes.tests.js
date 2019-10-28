import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import {
    cleanup,
    fireEvent,
    getAllByPlaceholderText,
    queryByPlaceholderText,
    find
} from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingRoutes from '../manufacturingRoutes/ManufacturingRoutes';

afterEach(cleanup);

const fetchItems = jest.fn();

const manufacturingRoutes = [
    {
        routeCode: 'TESTCODE1',
        description: 'Descripticon',
        cost: 10
    },
    {
        routeCode: 'TESTCODE2',
        description: 'Descrip',
        cost: 12
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: manufacturingRoutes,
    fetchItems,
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<ManufacturingRoutes {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<ManufacturingRoutes {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(<ManufacturingRoutes {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<ManufacturingRoutes {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });
    test('Should display both results', () => {
        const { getByText } = render(<ManufacturingRoutes {...defaultProps} />);
        expect(getByText('TESTCODE1')).toBeInTheDocument();
        expect(getByText('TESTCODE2')).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<ManufacturingRoutes {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });

    test('should display search bar', () => {
        const { getByPlaceholderText } = render(<ManufacturingRoutes {...defaultProps} />);
        const item = getByPlaceholderText('search..');
        expect(item).toBeInTheDocument();
    });

    // test('should call search upon text entry', () => {
    //     const { getByPlaceholderText } = render(<ManufacturingRoutes {...defaultProps} />);
    //     const item = getByPlaceholderText('search..');
    //     fireEvent.change(item, {
    //         target: { value: 'new value' }
    //     });
    //     expect(fetchItems).toHaveBeenCalledWith('new value');
    // });
});
