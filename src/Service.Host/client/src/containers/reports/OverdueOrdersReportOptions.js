import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import OverdueOrdersReportOptions from '../../components/reports/OverdueOrdersReportOptions';

const reportSelectors = new ReportSelectors('whoBuiltWhat');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(
    mapStateToProps,
    null
)(withRouter(OverdueOrdersReportOptions));
