import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import LabelReprint from '../../components/labelReprints/LabelReprint';
import labelReprintActions from '../../actions/labelReprintActions';
import labelReprintSelectors from '../../selectors/labelReprintSelectors';
import * as itemTypes from '../../itemTypes';
import labelTypesActions from '../../actions/labelTypesActions';
import labelTypesSelectors from '../../selectors/labelTypesSelectors';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';

const mapStateToProps = (state, { match }) => ({
    item: labelReprintSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: labelReprintSelectors.getEditStatus(state),
    loading: labelReprintSelectors.getLoading(state),
    snackbarVisible: labelReprintSelectors.getSnackbarVisible(state),
    applicationState: labelReprintSelectors.getApplicationState(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.labelReprint.item),
    labelTypes: labelTypesSelectors.getItems(state),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state, 100)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(labelReprintActions.fetch(itemId));
    dispatch(labelTypesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateItem: labelReprintActions.update,
    setEditStatus: labelReprintActions.setEditStatus,
    setSnackbarVisible: labelReprintActions.setSnackbarVisible,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    clearErrors: labelReprintActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelReprint));
