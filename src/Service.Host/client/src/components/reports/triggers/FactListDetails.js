import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import PropTypes from 'prop-types';

function FactListDetails({ details }) {
    return (
        <Table size="small">
            <TableBody>
                {details.map(d => (
                    <TableRow>
                        <TableCell>{d.header}</TableCell>
                        <TableCell>{d.value}</TableCell>
                        <TableCell>
                            {d.notes}
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

FactListDetails.propTypes = {
    details: PropTypes.arrayOf(PropTypes.shape({}))
};

FactListDetails.defaultProps = {
    details: []
};

export default FactListDetails;
