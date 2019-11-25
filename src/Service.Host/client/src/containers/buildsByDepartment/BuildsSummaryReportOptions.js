import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import BuildsSummaryReportOptions from '../../components/buildsbyDepartment/BuildsSummaryReportOptions';

const reportSelectors = new ReportSelectors('buildsSummary');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(mapStateToProps, null)(withRouter(BuildsSummaryReportOptions));
