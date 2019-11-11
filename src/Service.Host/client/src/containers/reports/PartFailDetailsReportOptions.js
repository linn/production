import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import PartFailDetailsReportOptions from '../../components/reports/PartFailDetailsReportOptions';
import partFailErrorTypesActions from '../../actions/partFailErrorTypesActions';
import partFailErrorTypesSelectors from '../../selectors/partFailErrorTypesSelectors';
import partFailFaultCodesActions from '../../actions/partFailFailFaultCodesActions';
import partFailFaultCodesSelectors from '../../selectors/partFailFaultCodesSelectors';
import departmentActions from '../../actions/departmentActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.partFailDetailsReport.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state),
    partFailErrorTypes: partFailErrorTypesSelectors.getItems(state),
    partFailErrorTypesLoading: partFailErrorTypesSelectors.getLoading(state),
    partFailFaultCodes: partFailFaultCodesSelectors.getItems(state),
    partFailFaultCodesLoading: partFailFaultCodesSelectors.getLoading(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    departments: departmentsSelectors.getItems(state),
    departmentsLoading: departmentsSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(partFailErrorTypesActions.fetch());
    dispatch(partFailFaultCodesActions.fetch());
    dispatch(partsActions.clearSearch());
    dispatch(departmentActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withRouter(initialiseOnMount(PartFailDetailsReportOptions)));
