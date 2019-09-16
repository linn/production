import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors } from '@linn-it/linn-form-components-library';
import SmtOutstandingWOPartsReportOptions from '../../components/reports/SmtOutstandingWOPartsReportOptions';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const reportSelectors = new ReportSelectors('smtOutstandingWorkOrderParts');

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
});

const mapDispatchToProps = {
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(SmtOutstandingWOPartsReportOptions));
