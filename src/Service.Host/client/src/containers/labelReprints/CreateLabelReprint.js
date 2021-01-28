import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import labelReprintActions from '../../actions/labelReprintActions';
import labelReprintStateActions from '../../actions/labelReprintStateActions';
import LabelReprint from '../../components/labelReprints/LabelReprint';
import labelReprintSelectors from '../../selectors/labelReprintSelectors';
import * as itemTypes from '../../itemTypes';
import labelTypesActions from '../../actions/labelTypesActions';
import labelTypesSelectors from '../../selectors/labelTypesSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = state => ({
    item: null,
    editStatus: 'create',
    itemError: getItemErrorDetailMessage(state, itemTypes.labelReprint.item),
    loading: labelReprintSelectors.getLoading(state),
    snackbarVisible: labelReprintSelectors.getSnackbarVisible(state),
    applicationState: labelReprintSelectors.getApplicationState(state),
    labelTypes: labelTypesSelectors.getItems(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state, 100)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
});

const initialise = () => dispatch => {
    dispatch(labelReprintActions.clearErrorsForItem());
    dispatch(labelTypesActions.fetch());
    dispatch(labelReprintStateActions.fetchState());
};

const mapDispatchToProps = {
    initialise,
    addItem: labelReprintActions.add,
    setEditStatus: labelReprintActions.setEditStatus,
    setSnackbarVisible: labelReprintActions.setSnackbarVisible,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    clearErrors: labelReprintActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelReprint));
