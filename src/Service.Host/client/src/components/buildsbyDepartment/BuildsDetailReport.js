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
import Page from '../../containers/Page';

function BuildsDetailReport({ reportData, loading, config, errorMessage, options }) {
    const href = `${config.appRoot}/production/reports/builds-detail/export?fromDate=${options.fromDate}&toDate=${options.toDate}&monthly=${options.monthly}&department=${options.department}&quantityOrValue=${options.quantityOrValue}`;

    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                <Grid item xs={8}>
                    <Title text={reportData ? reportData?.title?.displayString : 'Loading'} />
                </Grid>
                <Grid item xs={4}>
                    <ExportButton href={href} />
                </Grid>
                <Grid item xs={12}>
                    <Link component={RouterLink} to="/production/reports/builds-detail/options">
                        Run this report for different parameters
                    </Link>
                </Grid>
                <Grid item xs={12}>
                    {loading ? <Loading /> : ''}
                    <ReportTable
                        reportData={reportData}
                        showTotals
                        placeholderRows={10}
                        placeholderColumns={3}
                        showTitle={false}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

BuildsDetailReport.propTypes = {
    reportData: PropTypes.shape({}),
    options: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        monthly: PropTypes.string,
        department: PropTypes.string,
        quantityOrValue: PropTypes.string
    }).isRequired,
    config: PropTypes.shape({ appRoot: PropTypes.string }),
    loading: PropTypes.bool,
    errorMessage: PropTypes.string
};

BuildsDetailReport.defaultProps = {
    reportData: null,
    config: {},
    loading: false,
    errorMessage: ''
};

export default BuildsDetailReport;
