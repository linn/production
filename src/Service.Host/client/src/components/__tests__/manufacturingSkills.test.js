import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import ManufacturingSkills from '../manufacturingSkills/ManufacturingSkills';

afterEach(cleanup);

const manufacturingSkills = [
    {
        skillCode: 'TESTCODE1',
        description: 'Descripticon',
        hourlyRate: 10,
        dateInvalid: new Date().toISOString()
    },
    {
        skillCode: 'TESTCODE2',
        description: 'Descrip',
        hourlyRate: 12,
        dateInvalid: new Date().toISOString()
    }
];
const defaultProps = {
    loading: false,
    errorMessage: 'there was an error',
    items: manufacturingSkills,
    history: {}
};

describe('When Loading', () => {
    it('should display progress bar', () => {
        const { getAllByRole } = render(<ManufacturingSkills {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display table', () => {
        const { queryByRole } = render(<ManufacturingSkills {...defaultProps} loading />);
        expect(queryByRole('table')).not.toBeInTheDocument();
    });
});

describe('When viewing', () => {
    it('should not display progress bar', () => {
        const { queryByRole } = render(<ManufacturingSkills {...defaultProps} loading={false} />);
        expect(queryByRole('progressbar')).toBeNull();
    });

    test('Should display table', () => {
        const { queryByRole } = render(<ManufacturingSkills {...defaultProps} />);
        expect(queryByRole('table')).toBeInTheDocument();
    });

    test('should display the two manufactruing skills', () => {
        const { getByText } = render(<ManufacturingSkills {...defaultProps} />);
        const firstSkill = getByText('TESTCODE1');
        const secondSkill = getByText('TESTCODE2');
        expect(firstSkill).toBeInTheDocument();
        expect(secondSkill).toBeInTheDocument();
    });

    test('should display create button', () => {
        const { getByText } = render(<ManufacturingSkills {...defaultProps} />);
        const input = getByText('Create');
        expect(input).toBeInTheDocument();
    });
});
