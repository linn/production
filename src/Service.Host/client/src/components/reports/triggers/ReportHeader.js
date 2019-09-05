import React from 'react';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';

function ReportHeader({ citCode, citName, ptlJobref, ptlRunDateTime, reportFormat }) {
    return <Typography variant="h5">Cit {citName}</Typography>;
}

ReportHeader.propTypes = {
    ptlJobref: PropTypes.string,
    ptlRunDateTime: PropTypes.string,
    citCode: PropTypes.string,
    citName: PropTypes.string,
    reportFormat: PropTypes.string
};

ReportHeader.defaultProps = {
    ptlJobref: '',
    ptlRunDateTime: '',
    citCode: '',
    citName: '',
    reportFormat: ''
};

export default ReportHeader;
