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
    cits: [{ code: 'citcode1', name: 'citname1' }],
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

    test('should display part search bar', () => {
        const { getByPlaceholderText } = render(<ProductionTriggerLevels {...defaultProps} />);
        const item = getByPlaceholderText('search..');
        expect(item).toBeInTheDocument();
    });

    test('should display search boxes for override trigger and auto trigger', () => {
        const { getByText } = render(<ProductionTriggerLevels {...defaultProps} />);
        expect(getByText('Override Trigger >')).toBeInTheDocument();
        expect(getByText('Auto Trigger >')).toBeInTheDocument();
    });
});
