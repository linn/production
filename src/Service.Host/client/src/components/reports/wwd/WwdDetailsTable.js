import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';

function WwdDetailsTable({ details }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell>Part Number</TableCell>
                    <TableCell>Description</TableCell>
                    <TableCell>Qty Kitted</TableCell>
                    <TableCell>Reserved</TableCell>
                    <TableCell>Workstation Storage Place</TableCell>
                    <TableCell>Qty At Work Station</TableCell>
                    <TableCell />
                </TableRow>
            </TableHead>
            <TableBody>
                {details.map(m => (
                    <TableRow>
                        <TableCell>{m.partNumber}</TableCell>
                        <TableCell>{m.description}</TableCell>
                        <TableCell>{m.qtyKitted}</TableCell>
                        <TableCell>{m.qtyReserved}</TableCell>
                        <TableCell>{m.storagePlace}</TableCell>
                        <TableCell>{m.qtyAtLocation}</TableCell>
                        <TableCell>{m.remarks}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

WwdDetailsTable.propTypes = {
    details: PropTypes.shape({})
};

WwdDetailsTable.defaultProps = {
    details: null
};

export default WwdDetailsTable;
