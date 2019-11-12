import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import Page from '../../../containers/Page';
import WwdDetailsTable from './WwdDetailsTable';

function WwdTriggerReport({ reportData, loading, itemError, options }) {
    return (
        <Page>
            <Grid container spacing={3} justify="center">
                {loading ? (
                    <Grid item xs={12}>
                        <Title text="What Will Decrement From A Workstation" />
                        <Loading />
                    </Grid>
                ) : (
                    ''
                )}
                {itemError ? (
                    <ErrorCard errorMessage={itemError.details?.message} />
                ) : (
                    <Grid item xs={12}>
                        {reportData ? (
                            <Fragment>
                                <Title text="What Will Decrement From A Workstation" />
                                <span>{`${reportData.qty} x ${reportData.partNumber} from workstation ${reportData.workStationCode} `}</span>
                                {options.ptlJobref ? (
                                    <Link
                                        component={RouterLink}
                                        to={`/production/reports/triggers?jobref=${options.ptlJobref}&citCode=${options.citcode}`}
                                    >
                                        From trigger run {options.ptlJobref}
                                    </Link>
                                ) : (
                                    ''
                                )}
                                <WwdDetailsTable details={reportData.wwdDetails} />
                            </Fragment>
                        ) : (
                            ''
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

WwdTriggerReport.propTypes = {
    reportData: PropTypes.shape({}),
    loading: PropTypes.bool,
    itemError: PropTypes.shape({}),
    options: PropTypes.shape({})
};

WwdTriggerReport.defaultProps = {
    reportData: null,
    loading: false,
    itemError: null,
    options: null
};

export default WwdTriggerReport;
