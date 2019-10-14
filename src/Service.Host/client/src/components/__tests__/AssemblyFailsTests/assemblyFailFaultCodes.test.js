import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import AssemblyFailFaultCodes from '../../assemblyFails/AssemblyFailFaultCodes';

afterEach(cleanup);

const items = [
    {
        faultCode: 'CODE',
        description: 'DESC',
        explanation: 'EXPL'
    },
    {
        faultCode: 'EDOC',
        description: 'CSED',
        explanation: 'LPXE'
    }
];

const defaultProps = {
    items,
    loading: false,
    faultCodeLoading: false,
    snackbarVisible: false,
    setSnackbarVisible: jest.fn(),
    updateAssemblyFailFaultCode: jest.fn()
};

describe('when loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(<AssemblyFailFaultCodes {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(<AssemblyFailFaultCodes {...defaultProps} loading />);
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('when fault code item loading', () => {
    it('should display spinner', () => {
        const { getAllByRole } = render(
            <AssemblyFailFaultCodes {...defaultProps} faultCodeLoading />
        );
        expect(getAllByRole('progressbar').length).toBeGreaterThan(0);
    });

    it('should not display form fields', () => {
        const { queryByRole } = render(
            <AssemblyFailFaultCodes {...defaultProps} faultCodeLoading />
        );
        expect(queryByRole('input')).not.toBeInTheDocument();
    });
});

describe('when snackbar visible', () => {
    it('should render snackbar', () => {
        const { getByText } = render(<AssemblyFailFaultCodes {...defaultProps} snackbarVisible />);
        const item = getByText('Save Successful');
        expect(item).toBeInTheDocument();
    });
});

describe('when items have loaded', () => {
    it('should display all items', () => {
        const { getByText } = render(<AssemblyFailFaultCodes {...defaultProps} />);
        expect(getByText('CODE')).toBeInTheDocument();
        expect(getByText('EDOC')).toBeInTheDocument();
    });
});
