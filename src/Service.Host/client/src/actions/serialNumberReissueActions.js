import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { serialNumberReissueActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.serialNumberReissue.item,
    itemTypes.serialNumberReissue.actionType,
    itemTypes.serialNumberReissue.uri,
    actionTypes,
    config.appRoot
);
