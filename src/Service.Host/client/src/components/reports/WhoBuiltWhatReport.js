import React, { Fragment } from 'react';
import { Loading, Title, MultiReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import moment from 'moment';
import Page from '../../containers/Page';

const Results = ({ reportData }) => (
    <Fragment>
        {reportData.length === 0 ? (
            <div>No results returned for selected parameters</div>
        ) : (
            <MultiReportTable
                reportData={reportData}
                showTotals={false}
                placeholderRows={10}
                placeholderColumns={3}
                showRowTitles
                showTitle
            />
        )}
    </Fragment>
);

const WhoBuiltWhat = ({ reportData, loading, options }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                <Title
                    text={`Who built what between ${moment(options.fromDate).format(
                        'DD-MMM-YYYY'
                    )} and ${moment(options.toDate).format('DD-MMM-YYYY')} `}
                />
            </Grid>
            <Grid item xs={6}>
                {loading || !reportData ? <Loading /> : <Results reportData={reportData} />}
            </Grid>
            <Grid item xs={6} />
        </Grid>
    </Page>
);

Results.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({}))
};

Results.defaultProps = {
    reportData: []
};

WhoBuiltWhat.propTypes = {
    reportData: PropTypes.arrayOf(PropTypes.shape({})),
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

WhoBuiltWhat.defaultProps = {
    reportData: [],
    options: {},
    loading: false
};

export default WhoBuiltWhat;
