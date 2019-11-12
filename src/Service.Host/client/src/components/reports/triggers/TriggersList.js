import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import MenuItem from '@material-ui/core/MenuItem';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import ContextMenu from './ContextMenu';
import NotesPopover from './NotesPopover';

function TriggersList({ triggers, jobref, reportFormat }) {
    return (
        <Table size="small">
            <TableHead>
                <TableRow>
                    <TableCell>Part Number</TableCell>
                    <TableCell>Description</TableCell>
                    <TableCell>Build</TableCell>
                    <TableCell>Priority</TableCell>
                    <TableCell>Can Build</TableCell>
                    <TableCell>Kanban</TableCell>
                    <TableCell>Being Built</TableCell>
                    <TableCell>Story</TableCell>
                    <TableCell>Actions</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {triggers.map(m => (
                    <TableRow>
                        <TableCell>{m.partNumber}</TableCell>
                        <TableCell>{m.description}</TableCell>
                        <TableCell>
                            <Link
                                component={RouterLink}
                                to={`/production/reports/triggers/facts?jobref=${jobref}&part-number=${m.partNumber}`}
                            >
                                {m.remainingBuild
                                    ? m.remainingBuild
                                    : m.reqtForInternalAndTriggerLevelBT}
                            </Link>
                        </TableCell>
                        <TableCell>{m.priority}</TableCell>
                        <TableCell>
                            <Link
                                to={`/production/reports/wwd?part-number=${
                                    m.partNumber
                                }&ptlJobref=${jobref}&qty=${
                                    m.remainingBuild
                                        ? m.remainingBuild
                                        : m.reqtForInternalAndTriggerLevelBT
                                }`}
                            >
                                {m.canBuild}
                            </Link>
                        </TableCell>
                        <TableCell>{m.kanbanSize}</TableCell>
                        <TableCell>{m.qtyBeingBuilt}</TableCell>
                        <TableCell>
                            {m.story ? (
                                <NotesPopover id="story" tooltip="Story">
                                    {m.story}
                                </NotesPopover>
                            ) : (
                                ''
                            )}
                        </TableCell>
                        <TableCell>
                            <ContextMenu id={`triggers-menu${m.partNumber}`}>
                                <Link
                                    component={RouterLink}
                                    to={`/production/reports/triggers/facts?jobref=${jobref}&part-number=${m.partNumber}`}
                                >
                                    <MenuItem>Facts</MenuItem>
                                </Link>
                                <MenuItem>Edit Story</MenuItem>
                                <MenuItem>Works Order</MenuItem>
                                <MenuItem>Build Plan</MenuItem>
                            </ContextMenu>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

TriggersList.propTypes = {
    triggers: PropTypes.shape({}),
    reportFormat: PropTypes.string.isRequired,
    jobref: PropTypes.string.isRequired
};

TriggersList.defaultProps = {
    triggers: null
};

export default TriggersList;
