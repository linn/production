import React from 'react';
import { Loading, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const BoardTestsReport = ({ reportData, loading }) => (
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
                        placeholderRows={3}
                        placeholderColumns={6}
                        showRowTitles={false}
                    />
                )}
            </Grid>
        </Grid>
    </Page>
);

BoardTestsReport.propTypes = {
    reportData: PropTypes.shape({ title: PropTypes.string }),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

BoardTestsReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default BoardTestsReport;
