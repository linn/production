import React from 'react';
import moment from 'moment';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';

function WorksOrderList({ worksOrders }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell colSpan="4">Works Orders</TableCell>
                </TableRow>                
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
                        <Link
                                component={RouterLink}
                                to={`/production/works-orders/${o.orderNumber}`}
                            >
                                {o.orderNumber}
                            </Link>
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
