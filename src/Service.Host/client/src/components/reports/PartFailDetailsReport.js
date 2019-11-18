import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { ReportTable, Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

export default function PartFailDetailsReport({ reportData, loading, options, error }) {
    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Part Fail Details Report" />
                </Grid>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <Typography variant="subtitle2">
                        Error Type: {options.errorType}, Fault Code: {options.faultCode}, Part
                        Number: {options.partNumber}, From:{' '}
                        {moment(options.fromDate).format('DD-MMM-YYYY')}, To:{' '}
                        {moment(options.toDate).format('DD-MMM-YYYY')}
                    </Typography>
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

PartFailDetailsReport.propTypes = {
    reportData: PropTypes.shape({}),
    options: PropTypes.shape({ daysMethod: PropTypes.string }),
    loading: PropTypes.bool,
    error: PropTypes.string
};

PartFailDetailsReport.defaultProps = {
    reportData: null,
    options: {},
    loading: false,
    error: ''
};
