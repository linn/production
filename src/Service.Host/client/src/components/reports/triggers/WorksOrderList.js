import React from 'react';
import moment from 'moment';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Link from '@material-ui/core/Link';
import PropTypes from 'prop-types';

function WorksOrderList({ worksOrders }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell>Order Number</TableCell>
                    <TableCell>Qty</TableCell>
                    <TableCell>Qty Built</TableCell>
                    <TableCell>Date Started</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {worksOrders.map(o => (
                    <TableRow>
                        <TableCell>
                            <a href={`../../works-orders/:id/${o.orderNumber}`}>{o.orderNumber}</a>
                        </TableCell>
                        <TableCell>{o.quantity}</TableCell>
                        <TableCell>{o.quantityBuilt}</TableCell>
                        <TableCell>{moment(o.dateRaised).format('DD-MMM-YY HH:mm')}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

WorksOrderList.propTypes = {
    worksOrders: PropTypes.shape([{}])
};

WorksOrderList.defaultProps = {
    worksOrders: []
};

export default WorksOrderList;
