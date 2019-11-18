import React, { Fragment } from 'react';
import Grid from '@material-ui/core/Grid';
import { Loading, Title, ErrorCard } from '@linn-it/linn-form-components-library';
import Link from '@material-ui/core/Link';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import { Link as RouterLink } from 'react-router-dom';
import moment from 'moment';
import PropTypes from 'prop-types';
import Page from '../../../containers/Page';
import WwdDetailsTable from './WwdDetailsTable';

const useStyles = makeStyles(theme => ({
    padtop: {
        paddingTop: theme.spacing(6)
    },
    padbottom: {
        paddingBottom: theme.spacing(2)
    }
}));

function WwdTriggerReport({ reportData, loading, itemError, options }) {
    const classes = useStyles();

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
                                <div className={classes.padbottom}>
                                    <Typography>
                                        {`${reportData.qty} x ${reportData.partNumber} from workstation ${reportData.workStationCode} `}
                                    </Typography>
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
                                </div>

                                <WwdDetailsTable details={reportData.wwdDetails} />
                                <Typography
                                    className={classes.padtop}
                                    variant="caption"
                                    display="block"
                                    gutterBottom
                                >
                                    Based on run at {moment(reportData.wwdRunDatetime).format('DD-MMM HH:mm')} id{' '}
                                    {reportData.wwdJobId}
                                </Typography>
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
