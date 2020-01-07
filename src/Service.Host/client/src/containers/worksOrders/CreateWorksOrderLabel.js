import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import worksOrderLabelActions from '../../actions/worksOrderLabelActions';
import WorksOrderLabel from '../../components/worksOrders/WorksOrderLabel';
import worksOrderLabelSelectors from '../../selectors/worksOrderLabelSelectors';
import * as itemTypes from '../../itemTypes';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options || {};
};

const mapStateToProps = (state, ownProps) => ({
    item: { partNumber: getOptions(ownProps).partNumber },
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.worksOrderLabel.item),
    loading: worksOrderLabelSelectors.getLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    snackbarVisible: worksOrderLabelSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(worksOrderLabelActions.setEditStatus('create'));
    dispatch(worksOrderLabelActions.clearErrorsForItem());
};

const mapDispatchToProps = {
    initialise,
    addItem: worksOrderLabelActions.add,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    setEditStatus: worksOrderLabelActions.setEditStatus,
    setSnackbarVisible: worksOrderLabelActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(WorksOrderLabel));
