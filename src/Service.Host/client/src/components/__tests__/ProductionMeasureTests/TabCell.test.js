import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import TableBody from '@material-ui/core/TableBody';
import Table from '@material-ui/core/Table';
import TableRow from '@material-ui/core/TableRow';
import { cleanup } from '@testing-library/react';
import render from '../../../test-utils';
import TabCell from '../../reports/measures/TabCell';

const wrapInTable = children => (
    <Table>
        <TableBody>
            <TableRow>{children}</TableRow>
        </TableBody>
    </Table>
);

describe('<TabCell />', () => {
    afterEach(cleanup);

    describe('When same index', () => {
        it('should display tabcell contents', () => {
            const { getAllByText } = render(
                wrapInTable(
                    <TabCell index={1} value={1}>
                        test
                    </TabCell>
                )
            );
            expect(getAllByText('test').length).toBeGreaterThan(0);
        });
    });

    describe('When different index', () => {
        it('should display tabcell contents', () => {
            const { queryByText } = render(
                wrapInTable(
                    <TabCell index={2} value={1}>
                        {'test'}
                    </TabCell>
                )
            );
            expect(queryByText('test')).toBeNull();
        });
    });
});
