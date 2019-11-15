import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import WarningIcon from '@material-ui/icons/Warning';
import Tooltip from '@material-ui/core/Tooltip';
import PropTypes from 'prop-types';

function WwdDetailsTable({ details }) {
    function remarksIcon(d) {
        if (d.remarks.includes('totally SHORT')) {
            return (
                <Tooltip title={`${d.partNumber} is totally Short`}>
                    <WarningIcon color="error" />
                </Tooltip>
            );
        }
        return '';
    }

    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell />
                    <TableCell>Part Number</TableCell>
                    <TableCell>Description</TableCell>
                    <TableCell>Qty Kitted</TableCell>
                    <TableCell>Reserved</TableCell>
                    <TableCell>Workstation Storage Place</TableCell>
                    <TableCell>Qty At Work Station</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {details.map(m => (
                    <TableRow key={m.partNumber}>
                        <TableCell>{remarksIcon(m)}</TableCell>
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
    details: PropTypes.arrayOf(PropTypes.shape({}))
};

WwdDetailsTable.defaultProps = {
    details: null
};

export default WwdDetailsTable;
