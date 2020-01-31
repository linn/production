import React, { Fragment } from 'react';
import { Loading, ReportTable, Title, BackButton } from '@linn-it/linn-form-components-library';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { makeStyles } from '@material-ui/styles';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    subReport: {
        marginBottom: theme.spacing(4)
    },
    subTitle: {
        marginLeft: theme.spacing(2),
        fontWeight: 'bold'
    },
    spanRight: {
        marginRight: theme.spacing(4)
    },
    hide: {
        display: 'none'
    },
    floatRight: {
        float: 'right'
    }
}));

const handleBackClick = history => {
    history.push('/production/reports/measures');
};

function ShortageSummary({ summary, loading, history, options }) {
    const classes = useStyles();

    return (
        <Fragment>
            {loading || !summary ? (
                <Page>
                    <Loading />
                </Page>
            ) : (
                <Fragment>
                    <Page>
                        <Grid container spacing={3} justify="center">
                            <Grid item xs={12}>
                                <Title
                                    text={`${summary.citName} Shortages jobref ${options.ptlJobref}`}
                                />
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
                        </Grid>
                    </Page>
                    {summary.shortages.map(s => (
                        <Paper className={classes.subReport}>
                            <Grid className="padding-top-when-not-printing" container spacing={3}>
                                <Grid item xs={3}>
                                    <span className={classes.subTitle}>{s.partNumber}</span>
                                </Grid>
                                <Grid item xs={7}>
                                    <span className={classes.spanRight}>
                                        {`Priority ${s.priority}`}
                                    </span>
                                    <span className={classes.spanRight}>{`Build ${s.build}`}</span>
                                    <span className={classes.spanRight}>
                                        Can Build{' '}
                                        <Link
                                            component={RouterLink}
                                            to={`/production/reports/wwd?part-number=${s.partNumber}&ptlJobref=${options.ptlJobref}&citcode=${options.citCode}&qty=${s.build}`}
                                        >
                                            {s.canBuild}
                                        </Link>
                                    </span>
                                    <span className={classes.spanRight}>
                                        {`Back Order ${s.backOrderQty}`}
                                    </span>
                                    <span className={classes.spanRight}>
                                        {`Kanban ${s.kanban}`}
                                    </span>
                                </Grid>
                                <Grid item xs={2}>
                                    {s.earliestRequestedDate}
                                </Grid>
                                <ReportTable
                                    reportData={s.results.reportResults[0]}
                                    showTotals={false}
                                />
                            </Grid>
                        </Paper>
                    ))}
                    <Grid item xs={12}>
                        <BackButton backClick={() => handleBackClick(history)} />
                    </Grid>
                </Fragment>
            )}
        </Fragment>
    );
}

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
