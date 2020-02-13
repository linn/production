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

function TriggersList({ triggers, jobref, citcode }) {
    function build(t) {
        if (t.remainingBuild) {
            return t.remainingBuild;
        }
        return t.reqtForInternalAndTriggerLevelBT;
    }

    function canBuildText(t) {
        if (t.canBuild >= build(t) && build(t)) {
            return 'Yes';
        }

        if (!build(t) && t.canBuild >= t.qtyBeingBuilt) {
            return 'Yes';
        }
        return t.canBuild;
    }

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
                {triggers.map((m, i) => (
                    // eslint-disable-next-line react/no-array-index-key
                    <TableRow key={i}>
                        <TableCell>{m.partNumber}</TableCell>
                        <TableCell>{m.description}</TableCell>
                        <TableCell>
                            <Link
                                component={RouterLink}
                                to={`/production/reports/triggers/facts?jobref=${jobref}&part-number=${m.partNumber}`}
                            >
                                {build(m)}
                            </Link>
                        </TableCell>
                        <TableCell>{m.priority}</TableCell>
                        <TableCell>
                            <Link
                                component={RouterLink}
                                to={`/production/reports/wwd?part-number=${
                                    m.partNumber
                                }&ptlJobref=${jobref}&qty=${build(m) +
                                    m.qtyBeingBuilt}&citcode=${citcode}`}
                            >
                                {canBuildText(m)}
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
    triggers: PropTypes.arrayOf(PropTypes.shape({})),
    citcode: PropTypes.string,
    jobref: PropTypes.string.isRequired
};

TriggersList.defaultProps = {
    triggers: null,
    citcode: null
};

export default TriggersList;
