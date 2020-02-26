import React from 'react';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import {
    ReportTable,
    Loading,
    Title,
    ExportButton,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    topMargin: {
        marginTop: theme.spacing(3),
        marginRight: theme.spacing(2),
        marginLeft: theme.spacing(2)
    }
}));

function TimingsReport({ reportData, loading, config, errorMessage, options }) {
    const href = `${config.appRoot}/production/reports/mw-timings/export?startDate=${options.startDate}&endDate=${options.endDate}`;
    const classes = useStyles();

    return (
        <>
            <Page>
                <Grid container spacing={3} justify="center">
                    {errorMessage && (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={errorMessage} />
                        </Grid>
                    )}
                    <Grid item xs={8}>
                        <Title text={reportData?.title?.displayString} />
                    </Grid>
                    <Grid item xs={4}>
                        <ExportButton href={href} />
                    </Grid>
                    <Grid item xs={12}>
                        <Link component={RouterLink} to="/production/reports/mw-timings-setup">
                            Run this report for different dates
                        </Link>
                    </Grid>
                </Grid>
            </Page>
            <Grid item xs={12} className={classes.topMargin}>
                {loading ? <Loading /> : ''}
                <ReportTable
                    reportData={reportData}
                    showTotals
                    placeholderRows={10}
                    placeholderColumns={11}
                    showTitle
                />
            </Grid>
        </>
    );
}

TimingsReport.propTypes = {
    reportData: PropTypes.shape({}),
    options: PropTypes.shape({}).isRequired,
    config: PropTypes.shape({}),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

TimingsReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false,
    errorMessage: ''
};

export default TimingsReport;
