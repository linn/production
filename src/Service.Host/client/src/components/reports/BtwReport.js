import React from 'react';
import { Loading, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const BtwReport = ({ reportData, loading }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                {loading || !reportData ? (
                    <Loading />
                ) : (
                    <ReportTable
                        reportData={reportData}
                        title={reportData.title}
                        showTitle
                        showTotals={false}
                    />
                )}
            </Grid>
        </Grid>
    </Page>
);

BtwReport.propTypes = {
    reportData: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

BtwReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default BtwReport;
