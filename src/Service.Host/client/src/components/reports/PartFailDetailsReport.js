import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    ReportTable,
    Loading,
    ErrorCard,
    ExportButton
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

export default function PartFailDetailsReport({ reportData, config, loading, options, error }) {
    const href = `${config.appRoot}/production/quality/part-fails/detail-report/report/export?errorType=${options.errorType}&fromDate=${options.fromDate}&toDate=${options.toDate}&faultCode=${options.faultCode}&partNumber=${options.partNumber}&department=${options.department}`;

    return (
        <Page width="xl">
            <Grid container spacing={3}>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                <Grid item xs={12}>
                    <ExportButton href={href} />
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="subtitle2">
                        Options - Error Type: {options.errorType}, Fault Code: {options.faultCode},
                        Part Number: {options.partNumber}, From:{' '}
                        {moment(options.fromDate).format('DD-MMM-YYYY')}, To:{' '}
                        {moment(options.toDate).format('DD-MMM-YYYY')}
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
    config: PropTypes.shape({ appRoot: PropTypes.string }).isRequired,
    options: PropTypes.shape({
        daysMethod: PropTypes.string,
        errorType: PropTypes.string,
        partNumber: PropTypes.string,
        fromDate: PropTypes.string,
        toDate: PropTypes.string,
        faultCode: PropTypes.string,
        department: PropTypes.string
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
