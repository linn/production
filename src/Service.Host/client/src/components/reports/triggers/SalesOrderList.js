import React from 'react';
import moment from 'moment';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import PropTypes from 'prop-types';

function SalesOrderList({ salesOrders }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell colSpan="4">Sales Orders (external customes)</TableCell>
                </TableRow>
                <TableRow>
                    <TableCell>Order Number</TableCell>
                    <TableCell>Order Line</TableCell>
                    <TableCell>Qty On Order</TableCell>
                    <TableCell>Production Date</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {salesOrders.map(o => (
                    <TableRow>
                        <TableCell>
                            <a href={`../../../sales/orders/${o.orderNumber}`}>{o.orderNumber}</a>
                        </TableCell>
                        <TableCell>{o.orderLine}</TableCell>
                        <TableCell>{o.backOrderQty}</TableCell>
                        <TableCell>{moment(o.productionDate).format('DD-MMM-YY')}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

SalesOrderList.propTypes = {
    salesOrders: PropTypes.arrayOf(PropTypes.shape({}))
};

SalesOrderList.defaultProps = {
    salesOrders: []
};

export default SalesOrderList;
