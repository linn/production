import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { cleanup } from '@testing-library/react';
import render from '../../test-utils';
import AteFaultCodes from '../ate/AteFaultCodes';

afterEach(cleanup);

const addMock = jest.fn();
const updateMock = jest.fn();
const setEditStatusMock = jest.fn();
const items = [
    {
        faultCode: 0,
        description: 'description 1'
    },
    {
        faultCode: 1,
        description: 'description 2'
    }
];

const defaultProps = {
    loading: false,
    editStatus: 'view',
    addItem: addMock,
    updateItem: updateMock,
    setEditStatus: setEditStatusMock,
    history: { push: jest.fn() }
};

describe('When Loading', () => {
    it('should display heading and spinner', () => {
        const { getAllByRole } = render(<AteFaultCodes {...defaultProps} loading />);
        expect(getAllByRole('progressbar').length).toBe(1);
        expect(getAllByRole('heading').length).toBe(1);
    });
});

describe('When items arrive', () => {
    it('should not display spinner', () => {
        const { queryByRole } = render(
            <AteFaultCodes {...defaultProps} loading={false} items={items} />
        );
        expect(queryByRole('progressbar')).toBeNull();
    });

    it('should display table and create button', () => {
        const { getAllByRole, getByText } = render(
            <AteFaultCodes {...defaultProps} loading={false} items={items} />
        );
        expect(getAllByRole('table').length).toBe(1);
        expect(getByText('Create')).toBeInTheDocument();
    });
});
