import React from 'react';
import { Loading, ReportTable, Title } from '@linn-it/linn-form-components-library';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const ShortageSummary = ({ summary, loading, history, options }) => (
    <Page>
        {loading || !summary ? (
            <Loading />
        ) : (
            <Grid container spacing={3} justify="center">
                <Grid item xs={12}>
                    <Title text={`${summary.citName} Shortages jobref ${options.ptlJobref}`} />
                </Grid>
                <Grid item xs={2} />
                <Grid item xs={8}>
                    <Table size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell>1/2s</TableCell>
                                <TableCell>Shortages</TableCell>
                                <TableCell>BAT</TableCell>
                                <TableCell>Metalwork</TableCell>
                                <TableCell>Procurement</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            <TableRow>
                                <TableCell>{summary.onesTwos}</TableCell>
                                <TableCell>{summary.numShortages}</TableCell>
                                <TableCell>{summary.bAT}</TableCell>
                                <TableCell>{summary.metalwork}</TableCell>
                                <TableCell>{summary.procurement}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell />
                                <TableCell>{summary.percShortages}%</TableCell>
                                <TableCell>{summary.percBAT}%</TableCell>
                                <TableCell>{summary.percMetalwork}%</TableCell>
                                <TableCell>{summary.percProcurement}%</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </Grid>
                <Grid item xs={2} />
                {summary.shortages.map(s => (
                    <Grid className="padding-top-when-not-printing" container spacing={3}>
                        <Grid item xs={3}>
                            {s.partNumber}
                        </Grid>
                        <Grid item xs={7}>
                            {`Priority ${s.priority}   Build ${s.build}   Can Build ${s.canBuild}   Back Order ${s.backOrderQty}   Kanban ${s.kanban}`}
                        </Grid>
                        <Grid item xs={2}>
                            {s.earliestRequestedDate}
                        </Grid>
                        <ReportTable reportData={s.results.reportResults[0]} showTotals={false} />
                    </Grid>
                ))}
            </Grid>
        )}
    </Page>
);

ShortageSummary.propTypes = {
    summary: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

ShortageSummary.defaultProps = {
    summary: {},
    options: {},
    loading: false
};

export default ShortageSummary;
