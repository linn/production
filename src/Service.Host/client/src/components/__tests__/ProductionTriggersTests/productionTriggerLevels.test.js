import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import ProductionTriggerLevels from '../../productionTriggerLevels/TriggerLevels';

afterEach(cleanup);

const fetchItems = jest.fn();

const productionTriggerLevels = [
    {
        partNumber: 'partno1',
        description: 'Descripticon'
    },
    {
        partNumber: 'partno2',
        description: 'Descrip'
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: productionTriggerLevels,
    fetchItems,
    cits: [],
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<ProductionTriggerLevels {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<ProductionTriggerLevels {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(
            <ProductionTriggerLevels {...defaultProps} loading={false} />
        );
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<ProductionTriggerLevels {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });
    test('Should display both results', () => {
        const { getByText } = render(<ProductionTriggerLevels {...defaultProps} />);
        expect(getByText('partno1')).toBeInTheDocument();
        expect(getByText('partno2')).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<ProductionTriggerLevels {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });

    test('should display search bar', () => {
        const { getByPlaceholderText } = render(<ProductionTriggerLevels {...defaultProps} />);
        const item = getByPlaceholderText('search..');
        expect(item).toBeInTheDocument();
    });
});
