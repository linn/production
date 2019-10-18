import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import AssemblyFailFaultCodeRow from '../../assemblyFails/AssemblyFailFaultCodeRow';

afterEach(cleanup);

const updateMock = jest.fn();

const faultCode = {
    faultCode: 'CODE',
    description: 'DESC',
    explanation: 'EXPL',
    dateInvalid: '2019-10-14T11:39:26.0000000'
};

const defaultProps = {
    item: faultCode,
    updateAssemblyFailFaultCode: updateMock
};

describe('when viewing', () => {
    it('should display fault code data', () => {
        const { getByText } = render(<AssemblyFailFaultCodeRow {...defaultProps} />);
        expect(getByText(faultCode.faultCode)).toBeInTheDocument();
        expect(getByText(faultCode.description)).toBeInTheDocument();
        expect(getByText(faultCode.explanation)).toBeInTheDocument();
    });

    it('should display and format date field', () => {
        const { getByText } = render(<AssemblyFailFaultCodeRow {...defaultProps} />);
        expect(getByText('14 Oct 2019')).toBeInTheDocument();
    });

    it('should display edit button', () => {
        const { getByTestId } = render(<AssemblyFailFaultCodeRow {...defaultProps} />);
        expect(getByTestId('edit-button')).toBeInTheDocument();
    });

    it('should not display save and cancel buttons', () => {
        const { queryByTestId } = render(<AssemblyFailFaultCodeRow {...defaultProps} />);
        expect(queryByTestId('save-button')).toBeNull();
        expect(queryByTestId('cancel-button')).toBeNull();
    });
});
