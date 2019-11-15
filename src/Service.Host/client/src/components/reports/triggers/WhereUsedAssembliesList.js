import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import PropTypes from 'prop-types';

function WhereUsedAssembliesList({ assemblies }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell colSpan="7">Where Used (internal customers)</TableCell>
                </TableRow>
                <TableRow>
                    <TableCell>Part Number</TableCell>
                    <TableCell>Assembly Number</TableCell>
                    <TableCell>Qty Used</TableCell>
                    <TableCell>Ideal Build</TableCell>
                    <TableCell>Qty Being Built</TableCell>
                    <TableCell>Priority Build</TableCell>
                    <TableCell>Remaining Build Plan</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {assemblies.map(a => (
                    <TableRow>
                        <TableCell>{a.partNumber}</TableCell>
                        <TableCell>
                            <a href={`facts?jobref=${a.jobref}&part-number=${a.assemblyNumber}`}>
                                {a.assemblyNumber}
                            </a>
                        </TableCell>
                        <TableCell>{a.qtyUsed}</TableCell>
                        <TableCell>{a.reqtForInternalAndTriggerLevelBT}</TableCell>
                        <TableCell>{a.qtyBeingBuilt}</TableCell>
                        <TableCell>{a.reqtForInternalAndTriggerLevelBT}</TableCell>
                        <TableCell>{a.remainingBuild}</TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

WhereUsedAssembliesList.propTypes = {
    assemblies: PropTypes.arrayOf(PropTypes.shape({}))
};

WhereUsedAssembliesList.defaultProps = {
    assemblies: []
};

export default WhereUsedAssembliesList;
