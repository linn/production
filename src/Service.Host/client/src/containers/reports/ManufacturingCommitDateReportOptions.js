import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import ManufacturingCommitDateReportOptions from '../../components/reports/ManufacturingCommitDateReportOptions';

const reportSelectors = new ReportSelectors('manufacturingCommitDate');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state)
});

export default connect(mapStateToProps, null)(withRouter(ManufacturingCommitDateReportOptions));
