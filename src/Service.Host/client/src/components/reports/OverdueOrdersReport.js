import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { ReportTable, Loading, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

export default function OverdueOrdersReport({ reportData, loading, options }) {
    return (
        <Page width="xl">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Outstanding Sales Orders by Days Late" />
                </Grid>
                <Grid item xs={12}>
                    {options.daysMethod === 'Working Days' ? (
                        <Typography variant="subtitle2">Days Late are only working days</Typography>
                    ) : (
                        <Typography variant="subtitle2">
                            Days Late include weekends and holidays
                        </Typography>
                    )}
                </Grid>
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        <ReportTable reportData={reportData} showTotals={false} showTitle={false} />
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

OverdueOrdersReport.propTypes = {
    reportData: PropTypes.shape({}),
    options: PropTypes.shape({ daysMethod: PropTypes.string }),
    loading: PropTypes.bool
};

OverdueOrdersReport.defaultProps = {
    reportData: null,
    options: {},
    loading: false
};
