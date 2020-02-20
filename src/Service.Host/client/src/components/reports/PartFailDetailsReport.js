import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { ReportTable, Loading, ErrorCard } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

export default function PartFailDetailsReport({ reportData, loading, options, error }) {
    return (
        <Page width="xl">
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <Typography variant="subtitle2">
                        Options - Error Type: {options.errorType}, Fault Code: {options.faultCode},
                        Part Number: {options.partNumber}, From:{' '}
                        {moment(options.fromWeek).format('DD-MMM-YYYY')}, To:{' '}
                        {moment(options.toWeek).format('DD-MMM-YYYY')}
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    {loading ? (
                        <Loading />
                    ) : (
                        reportData && (
                            <ReportTable
                                reportData={reportData}
                                showTotals={false}
                                showTitle
                                title={reportData?.title.displayString}
                            />
                        )
                    )}
                </Grid>
            </Grid>
        </Page>
    );
}

PartFailDetailsReport.propTypes = {
    reportData: PropTypes.shape({}),
    options: PropTypes.shape({
        daysMethod: PropTypes.string,
        errorType: PropTypes.string,
        partNumber: PropTypes.string,
        fromWeek: PropTypes.string,
        toWeek: PropTypes.string,
        faultCode: PropTypes.string
    }),
    loading: PropTypes.bool,
    error: PropTypes.string
};

PartFailDetailsReport.defaultProps = {
    reportData: null,
    options: {},
    loading: false,
    error: ''
};
