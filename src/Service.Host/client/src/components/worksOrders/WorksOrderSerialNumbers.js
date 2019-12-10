import React from 'react';
import moment from 'moment';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import PropTypes from 'prop-types';

export default function WorksOrderSerialNumbers({ employees, serialNumbers, history }) {
    const findEmployeeName = employeeId => {
        const employee = employees.find(({ id }) => id === employeeId);
        return employee ? employee.fullName : '';
    };

    const handleIssueSernosClick = sernosNumber => {
        history.push(`/production/maintenance/serial-number-reissue?serialNumber=${sernosNumber}`);
    };

    return (
        <Grid item xs={12}>
            <Table size="small">
                <TableHead>
                    <TableRow>
                        <TableCell>Serial Number</TableCell>
                        <TableCell>Issued</TableCell>
                        <TableCell>By</TableCell>
                        <TableCell />
                    </TableRow>
                </TableHead>
                <TableBody>
                    {serialNumbers.map(sernos => (
                        <TableRow key={sernos.sernosNumber}>
                            <TableCell>{sernos.sernosNumber}</TableCell>
                            <TableCell>{moment(sernos.sernosDate).format('DD-MM-YYYY')}</TableCell>
                            <TableCell>{findEmployeeName(sernos.createdBy)}</TableCell>
                            <TableCell>
                                <Button
                                    color="primary"
                                    onClick={() => handleIssueSernosClick(sernos.sernosNumber)}
                                    variant="outlined"
                                >
                                    Issue Sernos
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Grid>
    );
}

WorksOrderSerialNumbers.propTypes = {
    employees: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    serialNumbers: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired
};
